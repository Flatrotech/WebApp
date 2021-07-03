using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApp.Api.Contracts;
using WebApp.Api.Models;

namespace WebApp.Api.Services
{
    public class WhitelistService : IWhitelistService
    {
        private readonly WebClient _client;
        private readonly IMojangService _mojangService;
        private readonly IConfiguration _config;

        public WhitelistService(IConfiguration config, IMojangService mojangService)
        {
            _client = new WebClient();
            _config = config;
            _mojangService = mojangService;
        }

        /// <summary>
        /// Adds user to the whitelist on the ftp server
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="newUser"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> AddToWhitelist(WhitelistModel whitelist)
        {
            UriBuilder ftp = new UriBuilder()
            {
                Host = whitelist.Host,
                UserName = whitelist.FtpUser,
                Port = whitelist.Port,
                Password = whitelist.Password,
                Path = _config["whitelist"],
                Scheme = Uri.UriSchemeFtp
            };
            
            try
            {
                await GetWhiteList(ftp.Uri, ftp.UserName, ftp.Password, _config["whitelist"]);

                await UpdateWhitelist(ftp.Uri, whitelist.NewUser, _config["whitelist"], ftp.UserName, ftp.Password);

                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// downloads a copy of the current version of
        /// the whitelist to the local directory
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// /// <param name="path"></param>
        /// <returns></returns>
        private async Task GetWhiteList(Uri ftp, string username, string password, string path)
        {
            FtpWebRequest request =
            (FtpWebRequest)WebRequest.Create(ftp);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.DownloadFile;

            WebResponse response = null;

            if (File.Exists(path))
                File.Delete(path);

            //in the case the download fails due to the file not existing on the 
            //server an empty version of the white list file will be generated
            //for writing to 
            try
            {
                response = await request.GetResponseAsync();

                using Stream ftpStream = response.GetResponseStream();

                using Stream fileStream = File.Create(path);

                ftpStream.CopyTo(fileStream);

                fileStream.Close();

                ftpStream.Close();
            }
            catch (Exception e)
            {
                Stream fileStream = File.Create(path);
                fileStream.Close();
            }
        }

        /// <summary>
        /// Adds text to whitelist in the local directory
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="whitelist"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task UpdateWhitelist(Uri ftp, string newUser, string whitelist, string username, string password)
        {
            using var writer = new StreamWriter(whitelist, true);
            await writer.WriteLineAsync($"{ newUser }");
            await DeleteWhitelist(ftp, username, password);
            writer.Close();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp);
            request.Credentials = new NetworkCredential(username, password);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            byte[] fileContents;
            using (StreamReader sourceStream = new StreamReader(whitelist))
            {
                fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            }

            request.ContentLength = fileContents.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(fileContents, 0, fileContents.Length);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                File.Delete(whitelist);
            }
        }

        /// <summary>
        /// Removes the whitelist file from the server 
        /// before the whitelist is reuploaded 
        /// </summary>
        /// <param name="ftp"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task DeleteWhitelist(Uri ftp, string username, string password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftp);
                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                var response = (FtpWebResponse)(await request.GetResponseAsync());
                response.Close();
            }
            catch (Exception e)
            {
                //File does not exist
            }
        }
    }
}
