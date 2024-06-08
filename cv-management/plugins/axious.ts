import axios from 'axios';
import { useToast } from 'vue-toast-notification';
import 'vue-toast-notification/dist/theme-bootstrap.css';
import https from '~/service/Base/axiousInstance';
import { formatTime, formatDate, getKeyByValue, splitArrayByStatusCV, capitalize, getVietNameseText, getAllowedUpdateStatus, checkIfAllowedUpdateStatus } from '~/assets/js/helper.js';
export default defineNuxtPlugin((nuxtApp) => {
    const toast = useToast();

    // Request interceptor
    https.interceptors.request.use(
        (config) => {
            config.headers.Authorization = `Bearer ${typeof localStorage !== 'undefined' ? localStorage.getItem('token') : ''}`;

            // Do something before request is sent
            config.headers.Authorization = `Bearer ${typeof localStorage !== 'undefined' ? localStorage.getItem('token') : ''}`;
            return config;
        },
        (error) => {
            // Handle request error
            toast.error('Request error: ' + error.message);
            const status = error.response ? error.response.status : null;

            if (status === 401) {
                // const newToken = "tempToken";
                // localStorage.setItem('authToken', newToken);
                // Retry the original request
                return axios(error.config);
            } else if (status === 404) {
                // Handle not found errors
            } else {
                // Handle other errors
            }
            return Promise.reject(error);
        },
    );

    // Response interceptor
    https.interceptors.response.use(
        (response) => {
            // Handle successful response
            if (response.config.method != 'get' && response.data.message) {
                toast.success('Request successful: ' + response.data.message);
            }
            if (response.config.method != 'get' && response.message) {
                toast.success('Request successful: ' + response.message);
            }
            return response;
        },
        (error) => {
            var messageError = '';

            // {field: 'Trường', message: 'Trường phải là bắt buộc!'} hoặc {message: 'Trường phải là bắt buộc!'} hoặc {status: 400,message: 'Trường phải là một số hợp lệ!'}
            if (Array.isArray(error.response.data)) {
                const errorMessageCombine = error.response.data.reduce(
                    (accumulator: any, currentValue: any) => {
                        return accumulator + currentValue.message;
                    },
                    messageError,

                );
                toast.error('Response error: <br/>' + errorMessageCombine);
            }
            else if (typeof error.response.data === 'string') {

                toast.error('Response error: <br/>' + error.response.data);
            }
            else {
                toast.error('Response error: <br/>' + error.response.data.message);
            }

            return error;
        },
    );

    // Provide the toast instance to your Vue components
    nuxtApp.provide('toast', toast);
});
