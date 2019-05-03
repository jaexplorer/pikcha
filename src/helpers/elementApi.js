import axios from 'axios';

axios.defaults.withCredentials = true;

// const apiVersion = 'v1';
const elementApi = axios.create({
    // baseURL: 'http://pikcha-server.test/api/v1/',
    baseURL: 'https://pikcha-server.matthewblode.com/api/v1/',
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json',
        'X-Requested-With': 'XMLHttpRequest'
    }
});

elementApi.interceptors.request.use(
    config => {
        return config;
    },
    error => Promise.reject(error)
);

// Add a response interceptor
elementApi.interceptors.response.use(
    response => {
        return response;
    },
    error => {
        return Promise.reject(error);
    }
);

export default elementApi;
