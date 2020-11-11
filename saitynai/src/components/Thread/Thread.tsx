import React, {useState, useEffect} from 'react'
import { useParams } from 'react-router-dom'
import { useApi } from '../../hooks/useAPI'
import './Thread.css'

interface IComment {
    id: number,
    body: string,
    userId: number,
    userEmail: string,
    threadId: number
}

interface IThread {
    id: number,
    title: string,
    body: string,
    userId: number,
    userEmail: string,
    categoryId: number,
    comments: IComment[]
}

interface IThreadParams {
    id: string
}

export const Thread: React.FunctionComponent = () => {
    const [thread, setThread] = useState<IThread>()
    const [error, setError] = useState<string>()
    const { id } = useParams<IThreadParams>()
    const { get } = useApi()

    useEffect(() => {
        const fetchThread = async () => {
            const result = await get(`api/thread/${id}`)

            if (result.code === 200 ) {
                setThread(JSON.parse(result.text))
            } else if (result.code > 200) {
                setError("Error fetching thread")
            }
        }

        fetchThread()
    }
    ,[get, id])

    return(
        <>
            {error ? <div className="threadError">{error}</div> : null}
            <div className={'threadContainer' + (error ? ' error' : '')}>
                <div className="threadTitle">{thread?.title}</div>
                <div className="threadBody">{thread?.body}</div>
                <div className="threadAuthor">by <b>{thread?.userEmail}</b></div>
                {thread?.comments.map(comment => 
                    <div className="threadComment" key={comment.id}>
                        {comment.body}
                        <div className="commentAuthor">by <b>{comment.userEmail}</b></div>
                    </div>
                )}
            </div>
        </>
    )
}
