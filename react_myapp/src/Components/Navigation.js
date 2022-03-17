import { Navbar, Nav, Container } from 'react-bootstrap';
import React from 'react';
import {LinkContainer} from 'react-router-bootstrap'

export default function Navigation(){
        return(
            <Navbar bg="light" expand="lg">
            <Container fluid>
                <Navbar.Toggle aria-controls="navbarScroll" />
                <Navbar.Collapse id="navbarScroll">
                <Nav
                    className="me-auto my-2 my-lg-0"
                    style={{ maxHeight: '100px' }}
                    navbarScroll
                >
                <LinkContainer to="/">
                    <Nav.Link>Home</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/page/">
                    <Nav.Link>Page</Nav.Link>
                </LinkContainer>
                <LinkContainer to="/snippet/">
                    <Nav.Link>Snippet</Nav.Link>
                </LinkContainer>
                </Nav>
                </Navbar.Collapse>
            </Container>
            </Navbar>
        );
}