import React from "react";
import Page from "./Components/Page/Page";
import Snippet from "./Components/Snippet/Snippet";
import Home from "./Components/Home/Home";
import Navigation from './Components/Navigation';
import {Route, Routes} from 'react-router';
import PageCreate from "./Components/Page/PageCreate";
import PageUpdate from "./Components/Page/PageUpdate";

function App() {
  return (
    <div>
      <Navigation/>
        <Routes>
          <Route path='/api/page/create/' element={<PageCreate/>}/>
          <Route path='/api/page/update/' element={<PageUpdate/>}/>
          <Route path='/' element={<Home/>}/>
          <Route path='/api/page/' element={<Page/>}/>
          <Route path='/api/snippet/' element={<Snippet/>}/>
        </Routes>
    </div>
  );
}

export default App;
