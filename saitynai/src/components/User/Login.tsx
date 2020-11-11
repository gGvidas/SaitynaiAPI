import React, {useState} from 'react'
import { useApi } from '../../hooks/useAPI'
import { useForm } from 'react-hook-form'
import { Login } from '../../utils/user'
import './Form.css'

type Login = {
    email: string,
    password: string
}

type LoginFormProps = {
    callback: () => void
}

export const LoginForm = ({callback}: LoginFormProps) => {
    const [error, setError] = useState<string>()
    const { register, handleSubmit } = useForm<Login>()
    const { post } = useApi()

    const onSubmit = async (login: Login) => {
        const result = await post('api/user/login', login)

        if (result.code === 200) {
            Login(JSON.parse(result.text))
            callback()
        } else {
            setError("Wrong email or password")
        }
    }

    return (
        <>
            {error ? error : null}
            <form onSubmit={handleSubmit(onSubmit)}>
                <label htmlFor="email">Email</label>
                <input name="email" type="email" ref={register}/>
                <label htmlFor="password">Password</label>
                <input name="password" type="password" ref={register}/>
                <input type="submit" value="Submit"/>
            </form>
        </>
    )
}