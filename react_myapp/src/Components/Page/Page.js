import React, {useEffect, useState} from 'react';
import {Button} from 'react-bootstrap';
import PageDelete from './PageDelete';
import {useNavigate} from 'react-router-dom';

export default function Page(){
    let [isPageGeted, setIsPageGeted] = useState(false);
    let [pageDeletedTriger, setPageDeleted] = useState(true);
    let [page, setPage] = useState({});
    let [items, setItems] = useState([]);
    const goPageCreate = useNavigate();

    useEffect(() => {
        console.log(pageDeletedTriger)
        fetch("/api/page/get")
            .then(responce => { return responce.json();})
            .then((pages) => {
                setItems(pages);
            });
    }, [pageDeletedTriger])
    
    function getPage(item){
        setIsPageGeted(true);
        setPage(item);
    }

    function closePage(){
        setIsPageGeted(false);
        setPage(null);
    }

    function pageDeleted(id)
    {
        setPageDeleted(prev => !prev);
        PageDelete(id);
    }

    if(!isPageGeted) return (
        <div>
            <Button variant='outline-success' size='sm' onClick={() => goPageCreate('/api/page/create/')}>Create new page</Button>
            <ul>
            {items.map(item =>
                <li key={item.id}>
                <div>{item.name}: {item.content}
                    <Button variant='outline-primary' size='sm' onClick={() => getPage(item)}>Watch</Button>
                    <Button variant='outline-warning' size='sm' onClick={() => goPageCreate('/api/page/update/', {state:{id: item.id}})}>Update</Button>
                    <Button variant='outline-danger' size='sm' onClick={() => pageDeleted(item.id)}>Delete</Button>
                </div>
                </li>
            )}
            </ul>
        </div>
    );
    else return (
            <div>
                <p><b>Id:</b> {page.id}</p>
                <p><b>Name:</b> {page.name}</p>
                <p><b>Content:</b> {page.content}</p>
                <p><Button variant='outline-primary' size='sm' onClick={() => closePage()}>Back</Button></p>
            </div>
        );
}