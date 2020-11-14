import React, { useState, useEffect} from 'react'
import { useApi } from '../../hooks/useAPI'
import { useParams } from 'react-router-dom'
import { ThreadLink } from './ThreadLink'
import './ThreadList.css'
import { ThreadForm } from './ThreadForm'

export interface IThread {
    id: number,
    title: string,
    body: string,
    userId: number,
    userEmail: string,
    categoryId: number
}

type ThreadListParams = {
    categoryId?: string
}

export const ThreadList:React.FunctionComponent = () => {
    const [isFormOpen, setIsFormOpen] = useState<boolean>(false)
    const [threads, setThreads] = useState<IThread[]>([])
    const { get } = useApi()
    const { categoryId } = useParams<ThreadListParams>()

    useEffect(() => {
        const fetchPosts = async () => {
            const result = await get(`api/thread${categoryId ? `/category/${categoryId}` : ''}`)

            if (result.code === 200) {
                setThreads(JSON.parse(result.text))
            }
        }

        fetchPosts()
    },[get, categoryId])

    return (
        <div className="threadList">
            <ThreadForm isOpen={isFormOpen} onRequestClose={() => setIsFormOpen(false)} callback={() => window.location.reload()} />
            <button onClick={() => setIsFormOpen(true)}>Create new</button>
            {threads.map(thread => <ThreadLink thread={thread} key={thread.id}/>)}
        </div>
    )
}