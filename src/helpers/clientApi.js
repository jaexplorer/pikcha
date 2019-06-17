import axios from 'axios';

axios.defaults.withCredentials = true;

// const apiVersion = 'v1';
const clientApi = axios.create({
    baseURL: 'http://pikcha-server.test/actions/api-module/',
    // baseURL: 'https://pikcha-server.matthewblode.com/actions/api-module/',
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        'X-Requested-With': 'XMLHttpRequest'
    }
});

clientApi.interceptors.request.use(
    config => {
        return config;
    },
    error => Promise.reject(error)
);

// Add a response interceptor
clientApi.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        return Promise.reject(error);
    }
);

export default clientApi;
