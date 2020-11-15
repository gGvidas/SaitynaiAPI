import React, { useState, useEffect } from 'react'
import { NavLink } from 'react-router-dom'
import { useApi } from '../../hooks/useAPI'
import { IsAdmin } from '../../utils/user'
import './CategoryList.css'

export interface ICategory {
    id: number,
    name: string
}

export const CategoryList = () => {
    const [categories, setCategories] = useState<ICategory[]>([])
    const { get } = useApi()

    useEffect(() => {
        const fetchCategories = async () => {
            const result = await get('api/category')
            if (result.code === 200) {
                setCategories(JSON.parse(result.text))
            }
        }

        fetchCategories()
    }, [get])
    
    return (
        <div className="categoryList">
            {categories.map(category => <NavLink to={`/${category.id}`} key={category.id} className="navbarLink" activeClassName="navbarLinkActive">{category.name}</NavLink>)}
            {IsAdmin() ? 
                <NavLink className="navbarLink" activeClassName="navbarLinkActive" to="/categories">Edit categories</NavLink>
            : null}
        </div>
    )
}