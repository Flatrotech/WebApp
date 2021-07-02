import React from 'react';
import ReactDOM from 'react-dom';
import './assets/css/index.css';
import App from './views/LandingPage/App';
import reportWebVitals from './assets/jss/reportWebVitals';
import { createBrowserHistory } from "history";
import { Router, Route, Switch } from "react-router-dom";

var hist = createBrowserHistory();

ReactDOM.render(
  <Router history={hist}>
    <Switch>
      <Route path="/" component={App} />
    </Switch>
  </Router>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
