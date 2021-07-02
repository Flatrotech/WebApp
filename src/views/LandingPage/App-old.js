import logo from './img/flatStudiosLogo.png';
import './App.css';
import { Button, Navbar } from 'react-bootstrap';

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <p className="App-name">
          Flat Studios
        </p>
        <Navbar className="App-nav">
          <Button variant="primary">Home</Button>{' '}
          <Button className="App-navItem">About</Button>
          <Button className="App-navItem">Contact</Button>
          <Button className="App-navItem">Craft</Button>
        </Navbar>
      </header>
      <body className="App-body">
        <div className="App-main">
          <div className="App-stack1">
            Testing
          </div>
        </div>
      </body>
    </div>
  );
}

export default App;
