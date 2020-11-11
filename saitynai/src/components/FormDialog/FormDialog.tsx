import React, { ReactNode } from 'react'
import ReactModal from 'react-modal'
import './FormDialog.css'

interface IFormDialogParams {
    isOpen: boolean,
    onRequestClose: () => any,
    children: ReactNode
}

export const FormDialog = ({isOpen, onRequestClose, children}: IFormDialogParams) => {
    return (
        <ReactModal
        isOpen={isOpen}
        overlayClassName="modalOverlay"
        className="modalBody"
        onRequestClose={onRequestClose}
        ariaHideApp={false}
        >
            {children}
        </ReactModal>
    )
}