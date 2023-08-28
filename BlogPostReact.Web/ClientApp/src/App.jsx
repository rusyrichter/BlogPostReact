import React from 'react';
import './App.css';
import { AuthContextComponent } from './AuthContextComponent';
import Layout from './Layout'
import Signup from './Signup'
import Home from './Home'
import Login from './Login'
import { Route, Routes } from 'react-router-dom';
import Logout from './Logout';
import PrivateRoute from './PrivateRoute'
import AskAQuestion from './AskAQuestion';
import ViewQuestionById from './ViewQuestionById';


class App extends React.Component {


    render() {
        return (
            <>
                <AuthContextComponent>
                    <Layout>
                        <Routes>
                            <Route exact path='/' element={<Home />} />
                            <Route exact path='/signup' element={<Signup />} />
                            <Route exact path='/login' element={<Login />} />
                            <Route exact path='/logout' element={
                                <PrivateRoute>
                                    <Logout />
                                </PrivateRoute>
                            } />
                            <Route exact path='/askaquestion' element={
                                <PrivateRoute>
                                    <AskAQuestion />
                                </PrivateRoute>
                            } />
                            <Route exact path='/viewquestionbyid/:questionId' element={                             
                                    <ViewQuestionById />                            
                            } />
                        </Routes>
                    </Layout>
                </AuthContextComponent>
            </>
        );
    }
};
export default App;