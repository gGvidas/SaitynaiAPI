import React, {useState, useEffect} from 'react'
import { useParams } from 'react-router-dom'
import { useForm } from 'react-hook-form'
import { useApi } from '../../hooks/useAPI'
import { GetEmail, GetId } from '../../utils/user'
import { Comment } from '../Comment/Comment'
import './Thread.css'

export interface IComment {
    id: number,
    body: string,
    userId: number,
    userEmail: string,
    threadId: number
}

interface INewComment {
    body: string,
    userId: number,
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
    const { register, handleSubmit } = useForm<INewComment>()
    const { id } = useParams<IThreadParams>()
    const { get, post } = useApi()

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

    const onSubmit = async (comment: INewComment) => {
        if (thread) {
            comment.threadId = thread?.id
            const userId = GetId()
            if (userId){
                comment.userId = userId
                await post('api/comment', comment)

                window.location.reload()
            }
        }
    }

    return(
        <>
            {error ? <div className="threadError">{error}</div> : null}
            <div className={'threadContainer' + (error ? ' error' : '')}>
                <div className="threadTitle">{thread?.title}</div>
                <div className="threadBody">{thread?.body}</div>
                <div className="threadAuthor">by <b>{thread?.userEmail}</b></div>
                {thread?.comments.map(comment => 
                    <Comment comment={comment} key={comment.id}/>
                )}
                {GetEmail() ?
                    <form className="newCommentForm" onSubmit={handleSubmit(onSubmit)}>
                        <label htmlFor="body">New comment:</label>
                        <textarea name="body" ref={register} />
                        <input type="submit" value="Comment"/>
                    </form>
                : null}
            </div>
        </>
    )
}
