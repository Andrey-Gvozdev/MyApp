export default function PageDelete(props){
    return (
        fetch('/page/delete/' + props, {
            method: 'DELETE'})
            .then(response => response.json())
    )
}