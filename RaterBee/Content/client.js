// All JavaScript in here will be loaded server-side
// Expose components globally so ReactJS.NET can use them

import App from './components/index.js';
//import Bootstrap from 'bootstrap/dist/css/bootstrap.css';

var Components = require('expose?Components!./components');