import React, { useEffect, useState } from 'react'
import { useApi } from '../../hooks/useAPI'
import edit from '../../icons/edit.svg'
import delIcon from '../../icons/delete.svg'
import img from '../../icons/Seoul.jpg'
import { ICategory } from './CategoryList'
import './AdminCategoryList.css'

export const AdminCategoryList: React.FunctionComponent<{}> = () => {
    const [isCreateNew, setIsCreateNew] = useState<boolean>()
    const [newCategoryTitle, setNewCategoryTitle] = useState<string>()
    const [categories, setCategories] = useState<ICategory[]>()
    const [editableCategory, setEditableCategory] = useState<number | null>(null)
    const [editableCategoryTitle, setEditableCategoryTitle] = useState<string>()
    const { get, post, patch, del } = useApi()

    useEffect(() => {
        const fetchCategories = async () => {
            const result = await get('api/category')
            if (result.code === 200) {
                setCategories(JSON.parse(result.text))
            }
        }

        fetchCategories()
    }, [get])

    const createNewCategory = async () => {
        await post('api/category', {name: newCategoryTitle})

        window.location.reload()
    }

    const editCategory = async () => {
        await patch(`api/category/${editableCategory}`, {name: editableCategoryTitle})

        window.location.reload()
    }

    const deleteCategory = async (id: number) => {
        await del(`api/category/${id}`)
    }

    return (
        <div className="categoryListDiv">
            <div className="newCategoryDiv"><button className="newCategoryButton" onClick={() => setIsCreateNew(!isCreateNew)}>Create a category</button></div>
            <table>
                <thead>
                    <tr>
                        <th>
                            Name
                        </th>
                        <th className="actionsColumn">
                            Actions
                        </th>
                    </tr>
                </thead>
                <tbody>
                    {categories?.map(category => 
                        <tr key={category.id}>
                            <td>
                                {editableCategory === category.id ? <input type="text" defaultValue={category.name} value={editableCategoryTitle} onChange={(e) => setEditableCategoryTitle(e.target.value)}/>:category.name}
                            </td>
                            <td className="actionsColumn">
                                {editableCategory === category.id ? 
                                    <button onClick={async () => editCategory()}>Edit</button>
                                :
                                    <>
                                        <button className="categoryListActionButton" onClick={() => setEditableCategory(category.id)}>
                                            <img alt="edit" src={edit}/>
                                        </button>
                                        <button className="categoryListActionButton" onClick={async () => deleteCategory(category.id)}>
                                            <img alt="delete" src={delIcon}/>
                                        </button>
                                    </>
                                }
                            </td>
                        </tr>    
                    )}
                    {isCreateNew ? 
                        <tr>
                            <td>
                                <input type="text" value={newCategoryTitle} onChange={(e) => setNewCategoryTitle(e.target.value)}/>
                            </td>
                            <td>
                                <button onClick={async () => createNewCategory()}>Create</button>
                            </td>
                        </tr>
                    : null}
                </tbody>
            </table>
            <img src={img} className="image"/>
        </div>
    )
}