import React, { ReactNode } from 'react'
import './Layout.css'

interface IProps {
    children: ReactNode
}

export const Layout = ({children}: IProps) => {
    const abc = () => {
        const abc = []
        for (let index = 0; index < 40000; index++) {
            abc.push(children)
        }
        return abc
    }

    return (
        <>
            <header>abc</header>
            <div className="body">{abc()}</div>
            <footer>abc</footer>
        </>
    )
} 