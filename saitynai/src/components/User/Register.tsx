import React, { useState } from 'react'
import { useApi } from '../../hooks/useAPI'
import { useForm } from 'react-hook-form'
import { Login } from '../../utils/user'
import { FormDialog } from '../FormDialog/FormDialog'
import './Form.css'

type Register = {
    email: string,
    password: string
}

interface IRegisterFormProps {
    isOpen: boolean,
    onRequestClose: () => any,
    callback: () => any
}

export const RegisterForm = ({isOpen, onRequestClose, callback}: IRegisterFormProps) => {
    const [error, setError] = useState<string>()
    const { register, handleSubmit } = useForm<Register>()
    const { post } = useApi()

    const onSubmit = async (register: Register) => {
        const result = await post('api/user/register', register)

        if (result.code === 201) {
            console.log("abc")
            Login(JSON.parse(result.text))
            callback()
        } else {
            setError("User already exists")
        }
    }

    return (
        <FormDialog isOpen={isOpen} onRequestClose={onRequestClose}>
            {error ? error : null}
            <form onSubmit={handleSubmit(onSubmit)}>
                <label htmlFor="email">Email</label>
                <input name="email" type="email" ref={register}/>
                <label htmlFor="password">Password</label>
                <input name="password" type="password" ref={register}/>
                <input type="submit" value="Submit"/>
            </form>
        </FormDialog>
    )
}