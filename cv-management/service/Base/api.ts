import api from './axiousInstance'
import port from '~/service/Base/http';
class Api {
    async get(url: string, config: any) {
        try {
            const response = await api.get(url, config)
            if (response == null) return null
            return response
        } catch (error: any) {
            console.log(error)
            return error.response.data
        }
    }
    async deleteById(url: string, id: number) {
        try {
            const result = await api.delete(`${url}/${id}`)
            if (result == null) return null
            return result
        } catch (error: any) {
            console.log(error)
            return error.response.data
        }
    }

    async putAPI(url: string, data: any) {
        try {
            const response = await api.put(`${url}`, data)
            if (response == null) return null
            return response
        } catch (error: any) {
            console.log(error)
            return error.response.data
        }
    }
    async postAPI(url: string, data: any) {
        try {
            const response = await api.post(`${url}`, data)
            if (response == null) return null
            return response
        } catch (error: any) {
            console.log(error)
            return error.response.data
        }
    }

    async postLogOut(url: string) {
        try {
            const response = await api.post(`${url}`, {})
            if (response == null) return null
            return response
        } catch (error: any) {
            console.log(error)
            return error.response.data
        }
    }
}
export default Api
