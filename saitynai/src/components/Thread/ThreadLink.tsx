import React from 'react'
import { Link } from 'react-router-dom'
import { IThread } from './ThreadList'
import './ThreadList.css'

type ThreadProps = {
    thread: IThread
}

export const ThreadLink: React.FunctionComponent<ThreadProps> = ({ thread }: ThreadProps) => {
    return (
        <div className="thread">
            <Link to={`/thread/${thread.id}`} className="threadLink">{thread.title}</Link>
            <div className="author">by {thread.userEmail}</div>
        </div>
    )
}