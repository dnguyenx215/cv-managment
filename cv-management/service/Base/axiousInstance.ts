import port from './http';
import axios, { type AxiosInstance } from 'axios';
const https: AxiosInstance = axios.create({
    baseURL: port.http,
    timeout: 10000,
    headers: {
        // "Content-type": "application/json",
        'Acess-Control-Allow-Origin': '*',
        Authorization: `Bearer ${typeof localStorage !== 'undefined' ? localStorage.getItem('token') : ''}`,
    },
});

export default https;
