export function getContent() {
    return window.localStorage.getItem('vue-cms-content') || '# Welcome\nThis is some default content';
  }
  
  export function setContent(content) {
    window.localStorage.setItem('vue-cms-content', content);
  }