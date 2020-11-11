import jwt_decode from 'jwt-decode'

type UserPayload = {
    Id: string,
    Admin: string,
    Email: string,
    exp: number
}

type User = {
    accessToken: string,
    refreshToken: string
}

type RefreshRequest = {
    email: string,
    refreshToken: string
}

export function Login(user: User) {
    localStorage.setItem('user', JSON.stringify(user))
}
export function Logout() {
    localStorage.removeItem('user')
}

export function GetAccessToken(): string | null {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)

        return parsedUser.accessToken
    }
    return null
}

export function IsExpired(): boolean {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = jwt_decode(parsedUser.accessToken) as UserPayload

        return payload.exp >= Date.now() / 1000
    }
    return true
}

export function IsAdmin(): boolean {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = jwt_decode(parsedUser.accessToken) as UserPayload

        return payload.Admin === "True"
    }
    return false
}

export function GetId(): number | null {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = jwt_decode(parsedUser.accessToken) as UserPayload

        return parseInt(payload.Id)
    }
    return null
}

export function GetEmail(): string | null {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = jwt_decode(parsedUser.accessToken) as UserPayload
        
        return payload.Email
    }
    return null
}

export function GetRefreshRequest(): RefreshRequest | null {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = jwt_decode(parsedUser.accessToken) as UserPayload
        
        return {email: payload.Email, refreshToken: parsedUser.refreshToken}
    }
    
    return null
}