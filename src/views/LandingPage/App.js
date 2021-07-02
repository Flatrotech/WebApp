import "assets/css/App.css"
import Header from 'components/Header/Header.js';
import HeaderLinks from 'components/Header/HeaderLinks.js';
import Button from "components/CustomButtons/Button.js";
import Parallax from "components/Parallax/Parallax.js";
import Footer from "components/Footer/Footer.js";
import GridContainer from "components/Grid/GridContainer.js";
import GridItem from "components/Grid/GridItem.js";

import classNames from "classnames";
import styles from "assets/jss/material-kit-react/views/landingPage.js";

import { makeStyles } from "@material-ui/core/styles";

const dashboardRoutes = [];
const useStyles = makeStyles(styles);

function App(props) {
  const classes = useStyles();
  const { ...rest } = props;
  return (
    <div className="App">
      <Header
        color="transparent"
        routes={dashboardRoutes}
        brand="Flatrotech's Minecraft Server"
        rightLinks={<HeaderLinks />}
        fixed
        changeColorOnScroll={{
          height: 400,
          color: "white",
        }}
        {...rest}
      />
      <Parallax filter image={require("assets/img/grass.jpg").default}>
        <div className={classes.container}>
          <GridContainer>
            <GridItem xs={12} sm={12} md={6}>
              <h1 className={classes.title}>Welcome to the Craft!</h1>
              <h4>
                Before you can start playing on the server you'll need to be 
                added to the official white list. Click the button below to 
                get added!
              </h4>
              <br />
              <Button
                color="info"
                size="lg"
                target="_blank"
                rel="noopener noreferrer"
              >
                <i className="fas fa-play" />
                Join Now
              </Button>
            </GridItem>
          </GridContainer>
        </div>
      </Parallax>
      <div className={classNames(classes.main, classes.mainRaised)}>
        <div className={classes.container}>
        </div>
      </div>
      <Footer />
    </div>
  );
}

export default App;
