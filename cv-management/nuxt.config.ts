// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
    devtools: { enabled: true },
    css: ['~/assets/css/styles/main.scss'],
    components: [
        {
            path: '~/components',
            pathPrefix: false,
        },
    ],
    modules: [
        'nuxt-bootstrap-icons',
        'nuxt-icon',
        '@vee-validate/nuxt',
        '@element-plus/nuxt',
        'usebootstrap',
        'nuxt-tiptap-editor',
        '@pinia/nuxt',
    ],
    pinia: {
        storesDirs: ['~/stores/**'],
    },
    tiptap: {
        prefix: "Tiptap", //prefix for Tiptap imports, composables not included

    },

    veeValidate: {
        autoImports: true,
    },
    build: {
        transpile: ['vee-validate'],
    },
    plugins: ['~/plugins/api', '~/plugins/vue-draggable',],
    app: {
        head: {
            title: 'CV Management',
            meta: [
                { charset: 'utf-8' },
                { name: 'viewport', content: 'width=device-width, initial-scale=1' },
                {
                    name: 'description',
                    content: process.env.npm_package_description || 'CV Management',
                },
            ],
            link: [
                { rel: 'icon', type: 'image/x-icon', href: '/favicon.ico' },

                {
                    rel: 'stylesheet',
                    href: 'https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css',
                },
                             {
                    rel: 'stylesheet',
                    href: 'https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css',
                },

            ],
            script: [
                {
                    src: 'https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js',
                    type: 'text/javascript',
                },
                { src: 'https://code.jquery.com/jquery-3.7.1.min.js' },
                { src: 'https://kit.fontawesome.com/efab6387bf.js' },


            ],
        },
    },
});
