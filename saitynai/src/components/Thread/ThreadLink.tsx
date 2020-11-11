import React from 'react'
import { Link } from 'react-router-dom'
import { IThread } from './ThreadList'
import './ThreadList.css'
import edit from '../../icons/edit.svg'
import del from '../../icons/delete.svg'
import { GetId, IsAdmin } from '../../utils/user'

type ThreadProps = {
    thread: IThread
}

export const ThreadLink: React.FunctionComponent<ThreadProps> = ({ thread }: ThreadProps) => {
    return (
        <Link to={`/thread/${thread.id}`} className="thread">
            <div className="threadLink">
                {thread.title}
                {(IsAdmin() || GetId() === thread.userId) ?
                <div className="threadActions">
                    <button className="threadActionButton">
                        <img alt="edit" src={edit}/>
                    </button>
                    <button className="threadActionButton">
                        <img alt="delete" src={del}/>
                    </button>
                </div>
                : null}
            </div>
            <div className="author">by {thread.userEmail}</div>
        </Link>
    )
}