import React, { useState } from 'react'
import { useApi } from '../../hooks/useAPI'
import { Link } from 'react-router-dom'
import { IThread } from './ThreadList'
import './ThreadList.css'
import edit from '../../icons/edit.svg'
import delIcon from '../../icons/delete.svg'
import { GetId, IsAdmin } from '../../utils/user'
import { ThreadForm } from './ThreadForm'

type ThreadProps = {
    thread: IThread
}

export const ThreadLink: React.FunctionComponent<ThreadProps> = ({ thread }: ThreadProps) => { 
    const [isFormOpen, setIsFormOpen] = useState<boolean>(false)
    const { del } = useApi()

    const deleteThread = async () => {
        const result = await del(`api/thread/${thread.id}`)

        if (result.code === 204) {
            window.location.reload()
        }
    }

    return (
        <div className="threadLinkWithActions">
            <ThreadForm oldThread={thread} isOpen={isFormOpen} onRequestClose={() => setIsFormOpen(false)} callback={() => window.location.reload()} />
            <Link to={`/thread/${thread.id}`} className="thread">
                <div className="threadLink">
                    {thread.title}
                </div>
                <div className="author">by {thread.userEmail}</div>
            </Link>
            {(IsAdmin() || GetId() === thread.userId) ?
                <div className="threadActions">
                    <button className="threadActionButton" onClick={() => setIsFormOpen(true)}>
                        <img alt="edit" src={edit}/>
                    </button>
                    <button className="threadActionButton" onClick={deleteThread}>
                        <img alt="delete" src={delIcon}/>
                    </button>
                </div>
            : null}
        </div>
    )
}