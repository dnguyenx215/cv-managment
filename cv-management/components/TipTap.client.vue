<script setup>

import { useEditor, EditorContent } from '@tiptap/vue-3'
import StarterKit from '@tiptap/starter-kit'
import Document from '@tiptap/extension-document'
import Paragraph from '@tiptap/extension-paragraph'
import Underline from '@tiptap/extension-underline'
import Link from '@tiptap/extension-link'
import HorizontalRule from '@tiptap/extension-horizontal-rule'
import Strike from '@tiptap/extension-strike'
import Text from '@tiptap/extension-text'
import TextStyle from '@tiptap/extension-text-style'
import { Node, Mark, mergeAttributes } from '@tiptap/core';

const props = defineProps({
    modelValue: String,
})
const SpanMark = Mark.create({
    name: 'SpanMark',
    priority: 110,
    parseHTML() {
        return [
            {
                tag: 'span'
            }
        ]
    },
    renderHTML() {
        return ['span', mergeAttributes({}), 0]
    }
})
const emit = defineEmits(['update:modelValue'])
const editor = useEditor({
    content: props.modelValue,
    onUpdate: ({ editor }) => {
        emit('update:modelValue', editor.getHTML())
    },
    extensions: [
        StarterKit,
        Highlight,
        Underline,
        Link,
        SpanMark],

    editorProps: {
        attributes: {
            class:
                'border border-secondary p-4 height-text-editor overflow-y-auto outline-none prose focus:outline-none form-control',
            id: 'textarea'
        },
    },
})

</script>
<template>
    <div class=" mb-2">
        <section v-if="editor" class="d-flex flex-wrap py-1">
            <button type="button" @click="editor.chain().focus().toggleBold().run()"
                :class="{ 'btn btn-outline-primary d-flex align-items-center': editor.isActive('bold') }"
                class="p-1 border-0 ">
                <Icon name="i-material-symbols:format-bold" title="Bold" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleItalic().run()"
                :class="{ 'btn btn-outline-primary d-flex align-items-center': editor.isActive('italic') }"
                class="p-1 border-0">
                <Icon name="i-material-symbols:format-italic" title="Italic" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleUnderline().run()"
                :class="{ 'btn btn-outline-primary d-flex align-items-center': editor.isActive('underline') }"
                class="p-1 border-0">
                <Icon name="i-material-symbols:format-underlined" title="Underline" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleHeading({ level: 1 }).run()" :class="{
                'btn btn-outline-primary d-flex align-items-center': editor.isActive('heading', { level: 1 }),
            }" class="p-1 border-0">
                <Icon name="i-material-symbols:format-h1-rounded" title="H1" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleHeading({ level: 2 }).run()" :class="{
                'btn btn-outline-primary d-flex align-items-center': editor.isActive('heading', { level: 2 }),
            }" class="p-1 border-0">
                <Icon name="i-material-symbols:format-h2-rounded" title="H2" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleBulletList().run()"
                :class="{ 'btn btn-outline-primary d-flex align-items-center': editor.isActive('bulletList') }"
                class="p-1 border-0">
                <Icon name="i-material-symbols:format-list-bulleted" title="Bullet" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleOrderedList().run()"
                :class="{ 'btn btn-outline-primary d-flex align-items-center': editor.isActive('orderedList') }"
                class="p-1 border-0">
                <Icon name="i-icon-park-solid:transaction-order" title="Order" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleBlockquote().run()"
                :class="{ 'btn btn-outline-primary d-flex align-items-center': editor.isActive('blockquote') }"
                class="p-1 border-0">
                <Icon name="i-material-symbols:format-quote" title="Blockquote" />
            </button>
            <button type="button" @click="editor.chain().focus().toggleCode().run()"
                :class="{ 'btn btn-outline-primary d-flex align-items-center': editor.isActive('code') }"
                class="p-1 border-0">
                <Icon name="i-material-symbols:code-blocks-outline" title="Blockcode" />
            </button>
            <button type="button" @click="editor.chain().focus().setHorizontalRule().run()" class="p-1 border-0">
                <Icon name="i-fluent:line-horizontal-1-28-filled" title="Horizontal" />
            </button>
            <button type="button" class="p-1 disabled:text-gray-400 border-0"
                @click="editor.chain().focus().undo().run()" :disabled="!editor.can().chain().focus().undo().run()">
                <Icon name="i-fluent:arrow-undo-16-filled" title="Undo" />
            </button>
            <button type="button" @click="editor.chain().focus().redo().run()"
                :disabled="!editor.can().chain().focus().redo().run()" class="p-1 disabled:text-gray-400 border-0">
                <Icon name="i-fluent:arrow-redo-16-filled" title="Redo" />
            </button>
        </section>
        <EditorContent :editor="editor" />
    </div>
</template>
<style scoped>
.menu-button {
    display: flex;
    align-items: center;
    justify-content: center;

    height: 32px;

    margin: 0;
    padding: 0 8px;

    border: 0;
    border-radius: 4px;
    background: transparent;
    color: currentColor;

    font-family: inherit;
    font-size: inherit;
    line-height: 1;
    font-weight: inherit;

    white-space: nowrap;
    cursor: pointer;
}

.menu-button {
    width: 32px;
    padding: 0;
}

.button:hover,
.button.is-active,
.menu-button:hover,
.menu-button.is-active {
    background-color: var(--color-gray-5);
    color: var(--color-black);
}

.button:disabled,
.menu-button:disabled {
    color: var(--color-gray-4);
}

.button-save {
    background-color: var(--color-green);
    color: var(--color-white);
}

.button-remove {
    background-color: var(--color-red);
    color: var(--color-white);
}

.button-remove:disabled {
    background-color: var(--color-pale-pink);
}

/* Basic editor styles */
.ProseMirror {
    margin-top: 4px;
    padding: 48px 8px 8px;

    border: 2px solid var(--color-gray-4);
    border-radius: 4px;
}

.editor-mini .ProseMirror {
    padding-top: 8px;
}

.ProseMirror-focused {
    border-color: var(--color-black);
    outline: none;
}

.ProseMirror>*+* {
    margin-top: 16px;
}
</style>
