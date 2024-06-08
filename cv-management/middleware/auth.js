export default defineNuxtRouteMiddleware((to, from) => {
    const router = useRouter();
    if (process.client) {
        const jwtToken = localStorage.getItem('token');

        if (!jwtToken || typeof jwtToken !== 'string' || jwtToken.trim() === '') {
            router.push('/').then();
            return ;
        }
    }
});