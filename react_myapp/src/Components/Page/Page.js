import React, {useEffect, useState} from 'react';
import {Button, Table} from 'react-bootstrap';
import PageDelete from './PageDelete';
import {useNavigate} from 'react-router-dom';

export default function Page(){
    let [items, setItems] = useState([]);
    const goPageCRU = useNavigate();

    useEffect(() => {
        fetch("/page/")
            .then(responce => { return responce.json();})
            .then((pages) => {
                setItems(pages);
            });
    }, [])

    function pageDeleted(id)
    {
        setItems(items.filter(item => {
            if (item.id !== id) {
                return item;
            }
            else return alert('Item deleted');
        }));
        PageDelete(id);
    }

    return (
        <div>
            <Button variant='success' size='sm' onClick={() => goPageCRU('/page/create/')}>Create new page</Button>
            <Table striped bordered hover size="sm">
                <thead>
                    <tr>
                    <th>#</th>
                    <th>Name</th>
                    <th>Content</th>
                    </tr>
                </thead>
                {items.map(item =>
                <tbody key={item.id}>
                    <tr>
                    <td>{item.id}</td>
                    <td>{item.name}</td>
                    <td>{item.content}</td>
                    <td>
                        <Button variant='primary' size='sm' onClick={() => goPageCRU('/page/update/' + item.id)}>Update</Button>
                        <Button variant='danger' size='sm' onClick={() => pageDeleted(item.id)}>Delete</Button>
                    </td>
                    </tr>
                </tbody>)}
            </Table>
        </div>
    );
}