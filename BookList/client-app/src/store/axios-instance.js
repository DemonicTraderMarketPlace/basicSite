import axios from 'axios'

const baseUrl = "https://localhost:44394/"

const instance = axios.create({
  baseURL: baseUrl + 'api/',
  contentType: 'application/json'
});

export default instance;
