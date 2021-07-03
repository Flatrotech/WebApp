import React from 'react';
import ReactDOM from 'react-dom';
import './assets/css/index.css';
import App from './views/LandingPage/App.js';
import SignUp from './views/SignUpPage/SignUp.js';
import Contact from './views/ContactPage/Contact.js';
import reportWebVitals from './assets/jss/reportWebVitals';
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

ReactDOM.render(
  <Router>
    <Switch>
      <Route exact path= "/" component = { App } />
      <Route path= "/signup" component = { SignUp } />
      <Route path= "/contact" component = { Contact } />
    </Switch>
  </Router>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
