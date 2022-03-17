import React, { useEffect, useState } from "react";
import {Button, Form} from 'react-bootstrap';
import {useNavigate, useParams} from 'react-router-dom';

export default function PageCRU(){
    let newPage = {
        content: '',
        name: '',
    };
    let [nameValue, setNameValue] = useState('');
    let [contentValue, setContentValue] = useState('');
    const goBack = useNavigate();
    const params = useParams();
    const prodId = params.id;
    let url = '';

    useEffect(() => {
        setUrl();
        if(prodId !== undefined){
        fetch(url)
            .then(responce => { return responce.json();})
            .then((page) => {
                setNameValue(page.name);
                setContentValue(page.content);
            });
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    function setUrl(){
        url = (prodId !== undefined) ? "/page/get/" + prodId :  "/page/get/";
    }

    function filingNewPage()
    {
        newPage.content = contentValue;
        newPage.name = nameValue;
    }

    function request(){
        filingNewPage();
        fetch(url, {
            method: (prodId !== undefined) ? "PUT" :  "POST",
            headers: {
              'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(newPage)
        })
            .then(function(res){ return res.json(); })
            .then(function(data){ alert(JSON.stringify(data)); goBack('/page/'); })
    }

    return(
        <Form>
            <Form.Group className="mb-3">
                <Form.Label>Page name</Form.Label>
                <Form.Control type='text' value={nameValue} onChange={e => setNameValue(e.target.value)}/>
            </Form.Group>

            <Form.Group className="mb-3">
                <Form.Label>Page content</Form.Label>
                <p><textarea className="form-control" rows="8" value={contentValue} onChange={e => setContentValue(e.target.value)}></textarea></p>
            </Form.Group>
        {prodId !== undefined 
        ? <Button variant='outline-primary' onClick={() => request()}>Update</Button>
        : <Button variant='outline-primary' onClick={() => request()}>Create</Button>}
            <Button variant='outline-primary' onClick={() => goBack('/page/')}>Back</Button>
        </Form>
    )
}