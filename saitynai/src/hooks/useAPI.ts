import { GetAccessToken, GetRefreshRequest, IsExpired, Login, Logout } from "../utils/user";

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
    const accessToken = GetAccessToken()
    const options: ApiOptions = {
        method: apiOptions.method,
        headers: {
            Authorization: `Bearer ${accessToken}`
        }
    }

    if (apiOptions.data) {
        options.headers['Content-Type'] = 'application/json'
        options.body = JSON.stringify(apiOptions.data)
    }

    const result = await fetch(`${getUrl()}/${apiOptions.path}`, options).then(res => res).catch(err => err)
    if (!result.ok) {
        if (IsExpired()) {
            const refreshResult = await fetch(`${getUrl()}/api/user/refresh`, {method: 'POST', headers:{
                'Content-Type': 'application/json'
            }, body: JSON.stringify(GetRefreshRequest())}).then(res => res).catch(err => err)
            if (refreshResult.ok) {
                Login(JSON.parse(await refreshResult.text()))

                return await send(apiOptions)
            } else if (refreshResult.status === 401) {
                Logout()
                window.location.reload()
                return { code: 0, text: ""}
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