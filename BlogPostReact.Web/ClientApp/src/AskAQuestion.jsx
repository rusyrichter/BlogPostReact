import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { useAuth } from './AuthContextComponent';
import { useNavigate } from 'react-router-dom';

const AsKAQuestion = () => {

    const navigate = useNavigate()

    const [title, setTitle] = useState('');
    const [text, setText] = useState('');
    const [questionTags, setTags] = useState([]);


    const handleTagInputChange = (e) => {
        const inputTags = e.target.value.split(',').map(tag => tag.trim());
        setTags(inputTags);
    }


    const onSubmitClick = async () => {
        await axios.post('/api/qa/askaquestion', {
            title,
            text,
            tags: questionTags
        });
        navigate('/');
    }

    return (
        <div className="container" b-k8y6yzo0ym="">
            <main className="pb-3" b-k8y6yzo0ym="" role="main">
                <div className="row" style={{ minHeight: '80vh', display: 'flex', alignItems: 'center' }}>
                    <div className="col-md-8 offset-md-2 bg-light p-4 rounded shadow">
                        <h2>Ask a question</h2>

                        <input value={title} onChange={e => setTitle(e.target.value)} className="form-control" name="title" placeholder="Title" />
                        <br />
                        <textarea value={text} onChange={e => setText(e.target.value)} className="form-control" placeholder="Type your question here..." rows="10" name="text" />
                        <br />
                        <input
                            type="text"
                            placeholder="Add tags..."
                            onChange={handleTagInputChange}
                        />
                        <br />
                        <button onClick={onSubmitClick} className="btn btn-primary">Submit</button>

                    </div>
                </div>
            </main>
        </div>
    );
}

export default AsKAQuestion;
