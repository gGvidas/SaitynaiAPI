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

const getToken = (): string => {
    return "abc"
}

const getUrl = ():string => {
    return "http://localhost/8000"
}

const getResponseBody = async (response: Response): Promise<any> => {
    const text = await response.text();
  
    try {
      return JSON.parse(text);
    } catch {
      return text;
    }
  };

const send = async (apiOptions: ApiParams) => {
    const options: ApiOptions = {
        method: apiOptions.method,
        headers: {}
    }
    options.headers.Authorization = `Bearer ${getToken()}`

    if (apiOptions.data) {
        options.headers['Content-Type'] = 'application/json'
        options.body = apiOptions.data
    }

    const result = await fetch(`${getUrl()}/${apiOptions.path}`, options)
    const responseBody = await getResponseBody(result)

    //TODO add token refresh and error

    return responseBody

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