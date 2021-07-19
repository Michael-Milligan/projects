#pragma once
#include <memory>
#include <iostream>

/// <summary>
/// Class for a node of the binary search tree
/// </summary>
template <class T>
struct node
{
	T data;
	std::shared_ptr<node<T>> pLeft;
	std::shared_ptr<node<T>> pRight;

	node(T data, std::shared_ptr<node<T>>& pLeft, std::shared_ptr<node<T>>& pRight);
	node();
};

template<class T>
using ptr = std::shared_ptr<node<T>>;

/// <summary>
/// Class for binary search tree
/// </summary>
template <class T>
class bst
{
private:
	ptr<T> root;
public:
	bst();
	ptr<T> search(T data);
	void add(T data);
	void output_tree(std::ostream);
};