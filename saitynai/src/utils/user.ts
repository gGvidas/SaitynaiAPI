function parseJwt(token: string) {
    var base64Url = token.split('.')[1]
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function(c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2)
    }).join(''))

    return JSON.parse(jsonPayload)
}

type UserPayload = {
    Id: number,
    Admin: boolean,
    Email: string
}

type User = {
    accessToken: string,
    refreshToken: string
}

export function Login(user: User) {
    localStorage.setItem('user', JSON.stringify(user))
}

export function IsAdmin(): boolean {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = parseJwt(parsedUser.accessToken)

        return payload.Admin
    }
    return false
}

export function GetId(): number | null {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = parseJwt(parsedUser.accessToken)

        return payload.Id
    }
    return null
}

export function GetEmail(): string | null {
    const user = localStorage.getItem('user')

    if (user) {
        const parsedUser: User = JSON.parse(user)
        const payload: UserPayload = parseJwt(parsedUser.accessToken)

        return payload.Email
    }
    return null
}