import React, { useState } from 'react';
import { useEffect } from 'react';
import { useAuth } from './AuthContextComponent';
import axios from 'axios'
import { Link, useParams } from 'react-router-dom';


const Home = () => {

    const [questions, setQuestions] = useState([]);

    useEffect(() => {
        const getQuestions = async () => {
            const response = await axios.get('/api/qa/getquestions');
            const data = response.data;
            setQuestions(data);
        }    
        getQuestions();
    }, []);

 
    return (

        <div className="container">
            <main role="main" className="pb-3">
                <div className="container">
                    <main role="main" className="pb-3">
                        <div className="row">
                            <div className="col-md-8 offset-md-2" style={{ marginTop: '100px' }}>
                                <div className="col-md-8 offset-md-2" style={{ marginTop: '100px' }}>
                                    {questions.map(question =>
                                        <div className="card card-body bg-light" key={question.id}>
                                            <h4>
                                                <Link to={`/viewquestionbyid/${question.id}`}>{question.title}</Link>
                                                
                                            </h4>
                                            <div>
                                               
                                                <span>Tags: </span> 
                                               
                                                <span className="badge bg-primary">  {question.questionsTags.map(qt => qt.tag.name).join(', ')} </span>

                                            </div>
                                            <div style={{ marginTop: '10px' }}>
                                                {question.text}
                                            </div>
                                            <span> {question.answers.length} answer(s)</span>
                                        </div>
                                    )}

                                </div>
                            </div>
                        </div>
                    </main>
                </div>
            </main>
        </div>
    );
}

export default Home;