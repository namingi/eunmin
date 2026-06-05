import { useEffect, useState } from "react";
import axios from "axios";

function App() {

const API = "http://localhost:5008/api/todo";

const [todos,setTodos] = useState([]);

const [title,setTitle] = useState("");

const [important,setImportant] = useState(1);



async function loadTodos(){

const res =
await axios.get(API);

setTodos(res.data);

}



useEffect(()=>{

loadTodos();

},[])



async function addTodo(){

if(title.trim()==="") return;

await axios.post(
API,
{
title:title,

important:Number(
important
),

isCompleted:false
}
);

setTitle("");

setImportant(1);

loadTodos();

}



async function toggleTodo(id){

await axios.put(
`${API}/${id}`
);

loadTodos();

}



async function deleteTodo(id){

await axios.delete(
`${API}/${id}`
);

loadTodos();

}



return(

<div style={{
padding:"30px",
maxWidth:"600px",
margin:"auto"
}}>

<h1>

Todo List

</h1>


<div>

<input

placeholder="Todo"

value={title}

onChange={
e=>setTitle(
e.target.value
)
}

/>


<input

type="number"

min="1"

max="5"

value={important}

onChange={
e=>setImportant(
e.target.value
)
}

/>

<button
onClick={addTodo}
>

ADD

</button>

</div>


<hr/>


{

todos.map(todo=>(

<div

key={todo.id}

style={{

display:"flex",

gap:"10px",

marginBottom:"10px",

alignItems:"center"

}}

>

<input

type="checkbox"

checked={todo.isCompleted}

onChange={()=>
toggleTodo(todo.id)
}

/>


<span style={{

textDecoration:

todo.isCompleted

?

"line-through"

:

"none"

}}

>

{todo.title}

(Priority:

{todo.important})

</span>


<button

onClick={()=>

deleteTodo(
todo.id
)

}

>

Delete

</button>

</div>

))

}

</div>

)

}

export default App;

