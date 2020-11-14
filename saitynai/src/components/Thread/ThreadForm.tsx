import React, {useState, useEffect} from 'react'
import { useApi } from '../../hooks/useAPI'
import { useForm } from 'react-hook-form'
import { FormDialog } from '../FormDialog/FormDialog'
import './ThreadForm.css'
import { IThread } from './ThreadList'
import { ICategory } from '../Category/CategoryList'

type Thread = {
    title?: string,
    body: string,
    categoryId?: number
}

interface IThreadFormProps {
    oldThread?: IThread
    isOpen: boolean,
    onRequestClose: () => any,
    callback: () => any
}

export const ThreadForm = ({oldThread, isOpen, onRequestClose, callback}: IThreadFormProps) => {
    const [error, setError] = useState<string>()
    const [categories, setCategories] = useState<ICategory[]>()
    const { register, handleSubmit } = useForm<Thread>()
    const { get, post, patch } = useApi()

    useEffect(() => {
        const fetchCategories = async () => {
            const result = await get("api/category")
            
            if (result.code === 200) {
                setCategories(JSON.parse(result.text))
            }
        }

        fetchCategories()
    }, [get])

    const onSubmit = async (thread: Thread) => {
        if (oldThread){
            const result = await patch(`api/thread/${oldThread.id}`, thread)

            if (result.code === 204) {
                callback()
            } else {
                setError("Error")
            }

        } else {
            const result = await post('api/thread', thread)

            if (result.code === 200) {
                callback()
            } else {
                setError("Error")
            }
        }
    }

    return (
        <FormDialog isOpen={isOpen} onRequestClose={onRequestClose}>
            {error ? error : null}
            <form onSubmit={handleSubmit(onSubmit)}>
                <label htmlFor="title">Title</label>
                <input name="title" type="text" ref={register} defaultValue={oldThread ? oldThread.title : ""} disabled={oldThread ? true : false}/>
                <label htmlFor="body">Body</label>
                <textarea name="body" ref={register} defaultValue={oldThread ? oldThread.body : ""}/>
                <label htmlFor="categoryId">Category</label>
                <select name="categoryId" disabled={oldThread ? true : false} ref={register}>
                    {categories?.map(category => <option value={category.id} key={category.id}>{category.name}</option>)}
                </select>
                <input type="submit" value="Submit"/>
            </form>
        </FormDialog>
    )
}