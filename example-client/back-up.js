const get_example = () => {

    fetch('http://localhost:8080/get')
        .then((r) => r.text())
        .then((d) => {
            console.log(d);
        });

};

const post_example = () => {

    const request = {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            attr_1: 'This is attr_1',
            attr_2: {
                inner_1: 'This is inner_1',
                inner_2: {
                    third_1: 'This is third_1',
                    third_2: 'This is third_2'
                }
            }
        })
    };

    fetch('http://localhost:8080/post', request)
        .then((r) => r.json())
        .then((d) => {
            console.log(JSON.stringify(d, null, '\t'));
        });

};