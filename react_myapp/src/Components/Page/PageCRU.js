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
        url = (prodId !== null) ? "/page/get/" + prodId :  "/page/get/";
    }

    function filingNewPage()
    {
        newPage.content = contentValue;
        newPage.name = nameValue;
    }

    function putRequest(){
        filingNewPage();
        fetch(url, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(newPage)
        })
            .then(function(res){ return res.json(); })
            .then(function(data){ alert(JSON.stringify(data)); goBack('/page/'); })
    }

    function postRequest(){
        filingNewPage();
        fetch(url, {
            method: 'POST',
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
        ? <Button variant='outline-primary' onClick={() => putRequest()}>Update</Button>
        : <Button variant='outline-primary' onClick={() => postRequest()}>Create</Button>}
            <Button variant='outline-primary' onClick={() => goBack('/page/')}>Back</Button>
        </Form>
    )
}