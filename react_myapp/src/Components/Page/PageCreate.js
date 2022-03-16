import React, { useState } from "react";
import {Button, Form} from 'react-bootstrap';
import {useNavigate} from 'react-router-dom';

export default function PageCreate(){
    let newPage = {
        content: '',
        name: '',
    };
    let [nameValue, setNameValue] = useState('');
    let [contentValue, setContentValue] = useState('');
    const goBack = useNavigate();

    function filingNewPage()
    {
        newPage.content = contentValue;
        newPage.name = nameValue;
    }

    function postRequest(){
        filingNewPage();
        fetch("/api/page/post/", {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(newPage)
        })
            .then(function(res){ return res.json(); })
            .then(function(data){ alert(JSON.stringify(data)); goBack('/api/page/'); })
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

            <Button variant='outline-primary' onClick={() => postRequest()}>Create</Button>
            <Button variant='outline-primary' onClick={() => goBack('/api/page/')}>Back</Button>
        </Form>
    )
}