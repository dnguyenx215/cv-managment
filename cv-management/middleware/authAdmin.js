import { jwtDecode } from 'jwt-decode';
import constants from '../assets/js/constants/constants.js'

export default defineNuxtRouteMiddleware((to, from) => {
    const router = useRouter();
    if (process.client) {
        if (to.path === '/account') {
            const jwtToken = localStorage.getItem('token');

            // Check if jwtToken is a string and not empty
            if (typeof jwtToken !== 'string' || jwtToken.trim() === '') {
                router.push('/').then();
                return; // Exit the middleware if no token is found
            }

            const decodedToken = jwtDecode(jwtToken);
            let role = decodedToken.Role;

            if (role.toLowerCase() !== constants.ADMIN.toLowerCase()) {
                router.push('/dashboard').then();
            }
        }
    }
});