import React from "react";
import Page from "./Components/Page/Page";
import Snippet from "./Components/Snippet/Snippet";
import Home from "./Components/Home/Home";
import Navigation from './Components/Navigation';
import {Route, Routes} from 'react-router';
import PageCRU from "./Components/Page/PageCRU";

function App() {
  return (
    <div>
      <Navigation/>
        <Routes>
          <Route path='/page/create/' element={<PageCRU/>}/>
          <Route path='/page/update/:id/' element={<PageCRU/>}/>
          <Route path='/' element={<Home/>}/>
          <Route path='/page/' element={<Page/>}/>
          <Route path='/snippet/' element={<Snippet/>}/>
        </Routes>
    </div>
  );
}

export default App;
