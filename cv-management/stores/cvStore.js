import { defineStore } from 'pinia'
export const useCvStore = defineStore('Cv', {
    state: () => ({
        selectedCvsToSendMail: [],
        emailsEditing: [],
        cvS: [],
    }),
    actions: {

    },
})
