import React, {useState} from 'react'
import { useForm } from 'react-hook-form'
import { IsAdmin, GetId } from '../../utils/user'
import { IComment } from '../Thread/Thread'
import { useApi } from '../../hooks/useAPI'
import './Comment.css'

interface ICommentProps {
    comment: IComment
}

interface IEditComment {
    body: string
}

export const Comment = ({comment}: ICommentProps) => {
    const [isEditable, setIsEditable] = useState<boolean>(false)
    const { register, handleSubmit } = useForm<IEditComment>()
    const { patch, del } = useApi()

    const deleteComment = async (id: number) => {
        await del(`api/comment/${id}`)

        window.location.reload()
    }

    const onSubmit = async (newComment : IEditComment) => {
        await patch(`api/comment/${comment.id}`, newComment)
        window.location.reload()
    }

    return (
    <div className="threadComment" key={comment.id}>
        {isEditable ? 
        <form className="editCommentForm" onSubmit={handleSubmit(onSubmit)}>
            <textarea name="body" ref={register} defaultValue={comment.body}/>
            <input type="submit" value="Edit"/>
        </form> 
        : comment.body }
        <div className="commentAuthor">by <b>{comment.userEmail}</b>
        {IsAdmin() || GetId() === comment.userId ? 
            <>
                <button className="commentActionButton" onClick={() => setIsEditable(!isEditable)}>Edit</button>
                <button className="commentActionButton" onClick={async () => await deleteComment(comment.id)}>Delete</button>    
            </>
        : null}
        </div>
    </div>
    )
}