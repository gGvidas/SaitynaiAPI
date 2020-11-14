import React, { ReactNode, useState } from 'react'
import { Link } from 'react-router-dom'
import './Layout.css'
import menu from '../../icons/menu.svg'
import { CategoryList } from '../Category/CategoryList'
import { GetEmail, Logout } from '../../utils/user'
import { LoginForm } from '../User/Login'
import { RegisterForm } from '../User/Register'

interface IProps {
    children: ReactNode
}

export const Layout: React.FunctionComponent<IProps> = ({children}: IProps) => {
    const [isNavbarOpen, setIsNavbarOpen] = useState<boolean>(false)
    const [isLoginModalOpen, setIsLoginModalOpen] = useState<boolean>(false)
    const [isRegisterModalOpen, setIsRegisterModalOpen] = useState<boolean>(false)

    const logout = () => {
        Logout()
        window.location.reload()
    }

    return (
        <>
            <LoginForm isOpen={isLoginModalOpen} onRequestClose={() => setIsLoginModalOpen(false)} callback={() => window.location.reload()}/>
            <RegisterForm isOpen={isRegisterModalOpen} onRequestClose={() => setIsRegisterModalOpen(false)} callback={() => window.location.reload()}/>
            <div className={"navbar navbarMobile" + (isNavbarOpen ? " navbarOpen" : "")}><CategoryList/></div>
            <div className={"body" + (isNavbarOpen ? " bodyOpen" : "")}>
                <header>
                    <button id="headerMenuButton" onClick={() => setIsNavbarOpen(!isNavbarOpen)}>
                        <img alt="" src={menu}/>
                    </button>
                    <Link to="/" className="homeLink">Forum</Link>
                    { GetEmail() ?
                        <div>
                            {GetEmail()}
                        </div>
                        :
                        <div>
                            <button onClick={() => setIsLoginModalOpen(true)}>
                                Login
                            </button>
                            <button onClick={() => setIsRegisterModalOpen(true)}>
                                Register
                            </button>
                        </div>
                    }
                </header>
                <div className="content">
                    <div className="navbarRegular">
                        <CategoryList/>
                    </div>
                    {children}
                </div>
                <footer>
                    Made by Gvidas Gaidauskas IFF-7/8
                    {GetEmail() ? <button onClick={() => logout()}>Logout</button> : null}
                </footer>
            </div>
        </>
    )
} 