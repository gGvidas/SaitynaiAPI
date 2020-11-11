import React, { useState, useEffect } from 'react'
import { useApi } from '../../hooks/useAPI'
import './CategoryList.css'

interface ICategory {
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
            {categories.map(category => <a key={category.id}>{category.name}</a>)}
        </div>
    )
}