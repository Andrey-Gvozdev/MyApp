export default function PageDelete(props){
    return (
        fetch('/api/page/delete/' + props, {
            method: 'DELETE'})
            .then(response => response.json())
    )
}