export default function PageDelete(props){
    return (
        fetch('/page/' + props, {
            method: 'DELETE'})
            .then(response => response.json())
    )
}