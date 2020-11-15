import React, { useEffect, useState } from 'react'
import { useApi } from '../../hooks/useAPI'
import edit from '../../icons/edit.svg'
import delIcon from '../../icons/delete.svg'
import img from '../../icons/Seoul.jpg'
import { ICategory } from './CategoryList'
import './AdminCategoryList.css'

export const AdminCategoryList: React.FunctionComponent<{}> = () => {
    const [categories, setCategories] = useState<ICategory[]>()
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
        <div className="categoryListDiv">
            <table>
                <tr>
                    <th>
                        Title
                    </th>
                    <th className="actionsColumn">
                        Actions
                    </th>
                </tr>
                {categories?.map(category => 
                    <tr>
                        <td>
                            {category.name}
                        </td>
                        <td className="actionsColumn">
                            <button className="categoryListActionButton">
                                <img alt="edit" src={edit}/>
                            </button>
                            <button className="categoryListActionButton">
                                <img alt="delete" src={delIcon}/>
                            </button>
                        </td>
                    </tr>    
                )}
            </table>
            <img src={img} className="image"/>
        </div>
    )
}