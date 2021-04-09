import React from 'react';

const Home = () => {
	return (
    <div>
      <div>I'm the Best component</div>
	  <button onClick={()=>console.log("HEY")}>Press me!</button>
    </div>
  );
};

export default Home;