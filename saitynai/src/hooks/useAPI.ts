import { GetAccessToken, GetRefreshRequest, IsExpired, Login } from "../utils/user";

type ApiParams = {
    method: string,
    path: string,
    data?: any 
}

type ApiOptions = {
    method: string,
    headers: any,
    body?: any
}

type FetchReturn = {
    code: number,
    text: string
}

function getUrl():string {
    return "http://localhost:8000"
}

const send = async (apiOptions: ApiParams): Promise<FetchReturn> => {
    const options: ApiOptions = {
        method: apiOptions.method,
        headers: {}
    }

    const accessToken = GetAccessToken()

    if (accessToken){
        options.headers.Authorization = `Bearer ${accessToken}`
    }

    if (apiOptions.data) {
        options.headers['Content-Type'] = 'application/json'
        options.body = apiOptions.data
    }

    const result = await fetch(`${getUrl()}/${apiOptions.path}`, options).then(res => res).catch(err => err)
    if (!result.ok) {
        if (IsExpired()) {
            const refreshResult = await fetch(`${getUrl()}/api/user/refresh`, {method: 'POST', body: JSON.stringify(GetRefreshRequest())}).then(res => res).catch(err => err)
            if (refreshResult.ok) {
                Login(JSON.parse(refreshResult.text()))

                return await send(apiOptions)
            }
        }
    }
    const text = await result.text()
    return { code: result.status, text: text }

}

const get = (path: string) => send({method: 'GET', path })
const post = (path: string, data: any) => send({method: 'POST', path, data})
const patch = (path: string, data: any) => send({method: 'PATCH', path, data})
const del = (path: string) => send({method: 'DELETE', path})

export const useApi = () => {
    return {
        get,
        post,
        patch,
        del
    }
}