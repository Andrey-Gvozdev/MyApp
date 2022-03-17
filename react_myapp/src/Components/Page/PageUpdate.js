import React, { useEffect, useState } from "react";
import {Button, Form} from 'react-bootstrap';
import {useLocation, useNavigate} from 'react-router-dom';

export default function PageUpdate(){
    let newPage = {
        content: '',
        name: '',
    };
    let [nameValue, setNameValue] = useState('');
    let [contentValue, setContentValue] = useState('');
    const goBack = useNavigate();
    const location = useLocation();

    useEffect(() => {
        fetch("/page/get/" + location.state.id)
            .then(responce => { return responce.json();})
            .then((page) => {
                setNameValue(page.name);
                setContentValue(page.content);
            });
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    function filingNewPage()
    {
        newPage.content = contentValue;
        newPage.name = nameValue;
    }

    function putRequest(){
        filingNewPage();
        fetch("/page/put/" + location.state.id, {
            method: 'PUT',
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

            <Button variant='outline-primary' onClick={() => putRequest()}>Update</Button>
            <Button variant='outline-primary' onClick={() => goBack('/page/')}>Back</Button>
        </Form>
    )
}